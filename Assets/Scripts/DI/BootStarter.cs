using System;
using LevelsSystem;
using Core.ControlSystem;
using GameplaySystem;
using PlayerSystem;
using UISystem;
using VContainer.Unity;
using VContainer;

namespace Game
{
    public class BootStarter : LifetimeScope
    {
		protected override void Configure(IContainerBuilder builder)
        {            
            builder.Register<UIController>(Lifetime.Scoped);
            builder.Register<ControlModule>(Lifetime.Scoped);
            builder.Register<GameplayController>(Lifetime.Scoped).As<GameplayController, IStartable>();
            builder.Register<PopupController>(Lifetime.Scoped).As<PopupController, IStartable>();
            builder.Register<LevelsController>(Lifetime.Scoped).As<LevelsController, IStartable, IDisposable, ITickable>();
            builder.Register<PlayerController>(Lifetime.Scoped).As<PlayerController, IStartable, IDisposable, ITickable>();

            builder.RegisterComponentInHierarchy<AssetLoader>();
		}       
    }
}