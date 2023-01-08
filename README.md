# Game feature:
   + Normal game mode: play as normal Moba game.
   + Player per matches: 10
   + Game phase : 
        + Pick hero phase
        + Prepare Phase
        + Play Phase
        + End game Phase....
        
        
# Development Environment:
    Unity: 2022.x
    Rider: 2022.3
    Source version controller: git / PlasticSCM
    Plugins: 
        + [UniTask](https://github.com/Cysharp/UniTask) : OK
        + DoTween
        + [VContainer](https://github.com/hadashiA/VContainer) (Considering)
        + PlayFab (Hope we can go until here)
        + [NaughtyAttribute](https://github.com/dbrizov/NaughtyAttributes) : OK
        + [ParallelSync](https://github.com/VeriorPies/ParrelSync) : OK
        + Odin??? (Maybe)
        + Rider Flow
    Model: 
        + Poly Perfect
        + Download Model From Dota official page
    Unity Packages: 
        + NGO: Networking Game Object 
        + New Input System : OK
        + Cinemachine
        + Addressable (Don't know if it support NGO)
        
        
    
    System architecture: 
        Take responsibility on the "underlayer" (common system that have scope)
        + GameInstance.
        + NetworkingSystem (NetworkingManager, Request class,..)
        + GameSceneManager 
        + AssetManager : Download Asset (Built-in, OnDemand), Load asset, check asset version, asset build pipeline,...
        + SoundManager (Sound Engineer)
        
        + UE like GamePlay framework
        
    GamePlay Engineer:
        + PlayerController
        + SkillSystem (implement hero skill)
        + Item System (implement how to use item, how item react from player to player, Shop stuff,....)
        + Timing spawn creeps, Tower attack,...
        
    UI Engineer:
        + UI relate task
        
        
        
Execute order rule:
    + LifeTimeScope exe order should be setup in this range: -10 -> -20
    
    
    
    
Note: 
    + Explain about GC of Unity (deep dive into GC of Unity)
    
    
    
    
    
    
    
      
        
        
        
        
    
        
        