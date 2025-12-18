using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gerencia o histórico de comandos executados
/// Permite Undo/Redo de ações
/// Singleton Pattern
/// 
/// COMO CONFIGURAR NO UNITY:
/// 1. Criar GameObject vazio na primeira cena: "CommandHistoryManager"
/// 2. Adicionar este componente CommandHistory
/// 3. Pronto! Ele persistirá entre cenas (DontDestroyOnLoad)
/// </summary>
public class CommandHistory : MonoBehaviour
{
    public static CommandHistory instance;

    // Stack para histórico de comandos executados
    private Stack<ICommand> commandHistory = new Stack<ICommand>();
    
    // Stack para comandos desfeitos (para Redo)
    private Stack<ICommand> redoStack = new Stack<ICommand>();

    [Header("Settings")]
    [SerializeField] private int maxHistorySize = 50;

    void Awake()
    {
        // Singleton Pattern
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        Debug.Log("[CommandHistory] Inicializado");
    }

    /// <summary>
    /// Executa um comando e adiciona ao histórico
    /// ESTE É O MÉTODO PRINCIPAL - Use este para executar todos os comandos!
    /// </summary>
    /// <param name="command">Comando a ser executado</param>
    public void ExecuteCommand(ICommand command)
    {
        if (command == null)
        {
            Debug.LogError("[CommandHistory] Comando nulo!");
            return;
        }

        // Executar o comando
        command.Execute();
        
        // Adicionar ao histórico
        commandHistory.Push(command);
        
        // Limpar redo ao executar novo comando
        redoStack.Clear();

        // Limitar tamanho do histórico
        if (commandHistory.Count > maxHistorySize)
        {
            var tempList = new List<ICommand>(commandHistory);
            tempList.RemoveAt(tempList.Count - 1);
            commandHistory = new Stack<ICommand>(tempList);
        }

        Debug.Log($"[CommandHistory] Executado: {command.GetDescription()} | Histórico: {commandHistory.Count}");
    }

    /// <summary>
    /// Desfaz o último comando executado
    /// </summary>
    public void UndoLastCommand()
    {
        if (commandHistory.Count > 0)
        {
            ICommand command = commandHistory.Pop();
            command.Undo();
            redoStack.Push(command);
            Debug.Log($"[CommandHistory] Undo: {command.GetDescription()}");
        }
        else
        {
            Debug.LogWarning("[CommandHistory] Nenhum comando para desfazer");
        }
    }

    /// <summary>
    /// Refaz o último comando desfeito
    /// </summary>
    public void RedoLastCommand()
    {
        if (redoStack.Count > 0)
        {
            ICommand command = redoStack.Pop();
            command.Execute();
            commandHistory.Push(command);
            Debug.Log($"[CommandHistory] Redo: {command.GetDescription()}");
        }
        else
        {
            Debug.LogWarning("[CommandHistory] Nenhum comando para refazer");
        }
    }

    /// <summary>
    /// Retorna o histórico completo de comandos
    /// </summary>
    /// <returns>Lista de descrições dos comandos</returns>
    public List<string> GetHistory()
    {
        List<string> history = new List<string>();
        foreach (var cmd in commandHistory)
        {
            history.Add(cmd.GetDescription());
        }
        return history;
    }

    /// <summary>
    /// Limpa todo o histórico
    /// </summary>
    public void ClearHistory()
    {
        commandHistory.Clear();
        redoStack.Clear();
        Debug.Log("[CommandHistory] Histórico limpo");
    }

    /// <summary>
    /// Retorna quantos comandos existem no histórico
    /// </summary>
    public int GetHistoryCount()
    {
        return commandHistory.Count;
    }

    /// <summary>
    /// Retorna quantos comandos podem ser refeitos
    /// </summary>
    public int GetRedoCount()
    {
        return redoStack.Count;
    }
}