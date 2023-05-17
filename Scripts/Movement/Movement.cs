using Godot;
using System;

public partial class Movement : CharacterBody3D, IMovement
{
    [Export]
    private float _moveSpeed = 30f;
    [Export]
    private float _jumpForce = 10f;

    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float JumpForce { get => _jumpForce; set => _jumpForce = value; }

    private Vector3 _velocity;
    // Get gravity 3D from config
    private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    private Vector3 Axis => new Vector3(Input.GetAxis("Right", "Left"), 0f, Input.GetAxis("Backward", "Forward"));

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        _velocity = Velocity;
        TranslateXZ((float)delta);
        _velocity.Y -= _gravity * (float)delta;
        if(Input.IsActionPressed("Jump") && IsOnFloor())
        {
            Jump();
        }
		Velocity = _velocity;
		MoveAndSlide();
    }

    public void TranslateXZ(float deltaTime)
    {
        _velocity = _velocity.Normalized();
        _velocity.X = Axis.X * MoveSpeed;
        _velocity.Z = Axis.Z * MoveSpeed;
        _velocity.Y = Velocity.Y;
    }

    public void Jump()
    {
        _velocity.Y = JumpForce;
    }
}
