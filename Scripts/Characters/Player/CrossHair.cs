using Godot;

[ Tool ]
public partial class CrossHair : Control
{
    [ Export ]
    private float _crossHairRadius = 3.0f;
    private float _crossHairShadowRadius = 4.0f;
    public override void _Draw() 
    {
        DrawCircle( Vector2.Zero, _crossHairShadowRadius, Colors.DimGray );
        DrawCircle( Vector2.Zero, _crossHairRadius, Colors.White );

        DrawLine( new Vector2(16,0), new Vector2(24,0), Colors.White, 2);
        DrawLine( new Vector2(0, 16), new Vector2(0, 24), Colors.White, 2);
        DrawLine( new Vector2(-16,0), new Vector2(-24,0), Colors.White, 2);
        DrawLine( new Vector2(0, -16), new Vector2(0, -24), Colors.White, 2);
    }
}
