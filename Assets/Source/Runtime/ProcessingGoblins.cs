//  Project : Actors-Examples
// Contacts : Pixeye - info@pixeye.games
//     Date : 2/20/2019 

using System;
using UnityEngine;

namespace Pixeye
{
	public class ProcessingGoblins : ProcessingBase, ITick
	{
		public Group<ComponentViewGoblin, ComponentState> group_brains;

		[GroupBy(Tag.StateAttack)]
		public Group<ComponentViewGoblin, ComponentWeapon> group_attack;

		[GroupBy(Tag.StateMove)]
		public Group<ComponentViewGoblin, ComponentMove> group_move;

		[GroupBy(Tag.StateIdle)]
		public Group<ComponentViewGoblin> group_idle;

		public ProcessingGoblins()
		{
			group_brains.Add += entity =>
			{
				var cState = entity.ComponentState();
				cState.current = Tag.StateIdle;
			};

			group_idle.Add += entity =>
			{
				var cState = entity.ComponentState();
				Timer.Add(1f, () => { cState.current = Tag.StateMove; });
				Debug.Log(String.Format("Goblin with id {0} is waiting !", entity));
			};

			group_move.Add += entity =>
			{
				var сMove = entity.ComponentMove();
				сMove.distanceToTarget = 5;
			};

			group_attack.Add += entity =>
			{
				var cWeapon = entity.ComponentWeapon();
				var cState  = entity.ComponentState();
				Debug.Log(String.Format("Goblin with id {0} performs an attack with mighty {1} !", entity, cWeapon.name));
				Timer.Add(1f, () => { cState.current = Tag.StateIdle; });
			};
		}

		public void Tick()
		{
			var frames = Time.frame;
			
			// process all entities that are defined in the groupBrains
			foreach (var entity in group_brains)
			{
				var cState = entity.ComponentState();

				if (cState.current != cState.previous)
				{
					if (cState.previous != -1)
						entity.Remove(cState.previous);

					cState.previous = cState.current;
					entity.Add(cState.current);
				}
			}

			foreach (var entity in group_move)
			{
				var сMove  = entity.ComponentMove();
				var cState = entity.ComponentState();
				if (frames % 20 == 0)
				{
					сMove.distanceToTarget = сMove.distanceToTarget.Minus(1);

					Debug.Log(String.Format("Goblin with id {0} moves to target. {1} meters left!", entity, сMove.distanceToTarget));

					if (сMove.distanceToTarget == 0) cState.current = Tag.StateAttack;
				}
			}
		}
	}
}