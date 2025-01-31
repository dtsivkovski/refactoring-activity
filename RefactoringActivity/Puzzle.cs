namespace RefactoringActivity;

public class Puzzle
{
    public string Name;
    public string Question;
    public string Answer;
        
    public Puzzle(string name, string question, string answer)
    {
        setName(name);
        this.setQuestion(question);
        this.setAnswer(answer);
    }
    
    public bool Solve()
    {
        Console.WriteLine($"Puzzle: {Question}");
        Console.Write("Your answer: ");
        string playerAnswer = Console.ReadLine()?.ToLower();
        return playerAnswer == Answer.ToLower();
    }
    
    private void setAnswer(string answer)
        {
            Answer = answer;
        }
    
        private void setQuestion(string question)
        {
            Question = question;
        }
    
        private void setName(string name)
        {
            Name = name;
        }
}