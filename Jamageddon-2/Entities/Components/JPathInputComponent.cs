using Jamageddon2.Scenes;
using Microsoft.Xna.Framework;
using MonoGame.Jolpango.Core;
using MonoGame.Jolpango.ECS;
using MonoGame.Jolpango.ECS.Components;
using MonoGame.Jolpango.Input;
using System;
using System.Collections.Generic;

namespace Jamageddon2.Entities.Components
{
    public class JPathInputComponent : JInputComponent, IJInjectable<Player>, IJInjectable<JSceneManager>
    {
        private List<Vector2> waypoints;
        private int currentWaypointIndex;
        private float waypointReachedDistance = 10f;
        private bool isMoving;
        private JTransformComponent transformComponent;
        private Player player;
        private JSceneManager sceneManager;

        public event Action OnPathComplete;
        public bool IsMoving => isMoving;

        public void Inject(Player service)
        {
            player = service;
        }
        public void Inject(JSceneManager service)
        {
            sceneManager = service;
        }
        public JPathInputComponent()
        {
            waypoints = new List<Vector2>();
            currentWaypointIndex = 0;
            isMoving = false;
        }

        public void SetPath(JPathComponent path)
        {
            waypoints = path.Waypoints;
            currentWaypointIndex = 0;
            isMoving = false;
        }

        public void StartMovement()
        {
            isMoving = true;
        }

        public void StopMovement()
        {
            isMoving = false;
            MoveIntent = Vector2.Zero;
        }

        public override void LoadContent()
        {
            transformComponent = Parent.GetComponent<JTransformComponent>();
        }

        public override void UpdateIntent(GameTime gameTime)
        {
            if (!isMoving || waypoints.Count == 0 || currentWaypointIndex >= waypoints.Count)
            {
                MoveIntent = Vector2.Zero;
                return;
            }

            Vector2 currentPosition = transformComponent.Position;
            Vector2 targetWaypoint = waypoints[currentWaypointIndex];
            Vector2 direction = targetWaypoint - currentPosition;

            if (direction.Length() <= waypointReachedDistance)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Count)
                {
                    isMoving = false;
                    MoveIntent = Vector2.Zero;
                    OnPathComplete?.Invoke();
                    player.TakeDamage(1); // Update with damage?
                    if (player.LivesLeft <= 0)
                        sceneManager.PopScene();
                    return;
                }
                targetWaypoint = waypoints[currentWaypointIndex];
                direction = targetWaypoint - currentPosition;
            }

            direction.Normalize();
            MoveIntent = direction;
        }

    }
}