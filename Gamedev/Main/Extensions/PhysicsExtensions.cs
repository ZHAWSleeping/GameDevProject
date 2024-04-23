using Godot;
using System;
using System.Linq;

namespace Gamedev.Main.Extensions
{
    public static class PhysicsExtensions
    {
        public static KinematicCollision2D[] GetSlideCollisions(this CharacterBody2D body) {
            return Enumerable.Range(0, body.GetSlideCollisionCount()).Select(index => body.GetSlideCollision(index)).ToArray();
        }

        public static bool HasNodesInGroup(this KinematicCollision2D[] collisions, StringName group) {
            return collisions.Select(collision => collision.GetCollider() as Node).Where(body => body.IsInGroup(group)).Any();
        }

        public static Node[] GetCollisions (this ShapeCast2D caster) {
            return Enumerable.Range(0, caster.GetCollisionCount()).Select(index => caster.GetCollider(index)).Cast<Node>().ToArray();
        }
    }
}