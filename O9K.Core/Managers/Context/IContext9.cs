namespace O9K.Core.Managers.Context
{
    using Assembly;

    using Ensage.SDK.Renderer;

    using Input;

    using Jungle;

    using Menu;

    using Particle;

    public interface IContext9
    {
        IAssemblyEventManager9 AssemblyEventManager { get; }

        IInputManager9 InputManager { get; }

        IJungleManager JungleManager { get; }

        IMenuManager9 MenuManager { get; }

        IParticleManager9 ParticleManger { get; }

        IRenderManager Renderer { get; }
    }
}