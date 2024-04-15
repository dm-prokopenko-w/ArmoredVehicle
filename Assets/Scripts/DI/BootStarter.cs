using System;
using LevelsSystem;
using Core.ControlSystem;
using GameplaySystem;
using PlayerSystem;
using ItemSystem;
using VContainer.Unity;
using VContainer;
using BattleSystem;
using EnemySystem;

namespace Game
{
    public class BootStarter : LifetimeScope
    {
		protected override void Configure(IContainerBuilder builder)
        {            
            builder.Register<ItemController>(Lifetime.Scoped);
            builder.Register<ControlModule>(Lifetime.Scoped);
            builder.Register<BattleController>(Lifetime.Scoped);
            builder.Register<GameplayController>(Lifetime.Scoped).As<GameplayController, IStartable>();
            builder.Register<PopupController>(Lifetime.Scoped).As<PopupController, IStartable>();
            builder.Register<LevelsController>(Lifetime.Scoped).As<LevelsController, IStartable, IDisposable, ITickable>();
            builder.Register<EnemyController>(Lifetime.Scoped).As<EnemyController, IStartable, IDisposable, ITickable>();
            builder.Register<PlayerController>(Lifetime.Scoped).As<PlayerController, IStartable, IDisposable, ITickable>();

            builder.RegisterComponentInHierarchy<AssetLoader>();
		}       
    }
}