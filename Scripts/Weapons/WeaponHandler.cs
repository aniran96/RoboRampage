using Godot;

public partial class WeaponHandler : Node3D
{
    [ Export ]
    private Node3D _weapon1;
    [ Export ]
    private Node3D _weapon2;

    public override void _Ready()
    {
        Equip( _weapon2 );
    }

    private void Equip( Node3D activeWeapon ) 
    {
        foreach ( Node3D child in GetChildren() ) 
        {
            if ( child == activeWeapon ) 
            {
                child.Visible = true;
                child.SetProcess( true );
            }
            else 
            {
                child.Visible = false;
                child.SetProcess( false );
            }
        }
    }
}
