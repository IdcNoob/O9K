namespace O9K.Core.Managers.Context
{
    using System;
    using System.ComponentModel.Composition;

    using Assembly;

    using Ensage.SDK.Renderer;

    using Input;

    using Jungle;

    using Menu;

    using Particle;

    [Export(typeof(IContext9))]
    public sealed class Context9 : IContext9
    {
        private readonly Lazy<IAssemblyEventManager9> assemblyEventManager;

        private readonly Lazy<IInputManager9> inputManager;

        private readonly Lazy<IJungleManager> jungleManager;

        private readonly Lazy<IMenuManager9> menuManager;

        private readonly Lazy<IParticleManager9> particleManger;

        private readonly Lazy<IRenderManager> renderer;

        [ImportingConstructor]
        public Context9(
            Lazy<IAssemblyEventManager9> assemblyEventManager,
            Lazy<IRenderManager> renderer,
            Lazy<IMenuManager9> menuManager,
            Lazy<IInputManager9> inputManager,
            Lazy<IJungleManager> jungleManager,
            Lazy<IParticleManager9> particleManger)
        {
            this.assemblyEventManager = assemblyEventManager;
            this.renderer = renderer;
            this.menuManager = menuManager;
            this.inputManager = inputManager;
            this.jungleManager = jungleManager;
            this.particleManger = particleManger;
        }

        public IAssemblyEventManager9 AssemblyEventManager
        {
            get
            {
                return this.assemblyEventManager.Value;
            }
        }

        public IInputManager9 InputManager
        {
            get
            {
                return this.inputManager.Value;
            }
        }

        public IJungleManager JungleManager
        {
            get
            {
                return this.jungleManager.Value;
            }
        }

        public IMenuManager9 MenuManager
        {
            get
            {
                return this.menuManager.Value;
            }
        }

        public IParticleManager9 ParticleManger
        {
            get
            {
                return this.particleManger.Value;
            }
        }

        public IRenderManager Renderer
        {
            get
            {
                return this.renderer.Value;
            }
        }
    }
}