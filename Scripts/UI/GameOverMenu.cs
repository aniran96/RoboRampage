using Godot;

public partial class GameOverMenu : Control
{

	// Exported nodes
	[Export]
	private Button _restartButtonNode;
	[Export]
	private Button _quitButtonNode;

	public override void _Ready()
	{
		_restartButtonNode.Pressed += OnRestartButtonPressed;
		_quitButtonNode.Pressed += OnQuitButtonPressed;
	}

    

    public override void _Process(double delta)
	{
	}

	public void GameOver() 
	{
		GetTree().Paused = true;
		Visible = true;
		Input.MouseMode = Input.MouseModeEnum.Visible;
	}

	private void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }

    private void OnRestartButtonPressed()
    {
		GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
    }
}
