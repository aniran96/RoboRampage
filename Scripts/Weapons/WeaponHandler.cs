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

    public override void _UnhandledInput(InputEvent evt)
    {
        if ( evt.IsActionPressed(GameConstants.WEAPON_1) ) 
        {
            Equip( _weapon1 );
        }        
        else if ( evt.IsActionPressed( GameConstants.WEAPON_2 ) ) 
        {
            Equip( _weapon2 );
        }
        if ( evt.IsActionPressed(GameConstants.NEXT_WEAPON) ) 
            {
                NextWeapon();
            }
    }

    private void NextWeapon() 
    {
        var index = GetCurrentIndex();
        index = Mathf.Wrap( index +1, 0, GetChildren().Count);
        Equip(GetChild<Node3D>(index));
    }

    private int GetCurrentIndex() 
    {
        for ( int index = 0; index < GetChildren().Count; index++ ) 
        {
            if ( GetChild<Node3D>(index).Visible ) 
            {
                return index;
            }
        }
        return 0;
    }
}
