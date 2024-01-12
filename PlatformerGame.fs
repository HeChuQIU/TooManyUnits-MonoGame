module PlatformerGame
 
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open PlatformerActors
 
type Game1 () as x =
    inherit Game()

    do x.Content.RootDirectory <- "Content"
    do x.IsMouseVisible <- true
 
    let graphics = new GraphicsDeviceManager(x)
    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>
    let CreateActor' = CreateActor x.Content
    let WorldObjects = lazy ([("player", Player(Nothing), Vector2(10.f,28.f), Vector2(32.f,32.f), false);
                          ("obstacle", Obstacle, Vector2(10.f,60.f), Vector2(32.f,32.f), true);
                          ("", Obstacle, Vector2(42.f,60.f), Vector2(32.f,32.f), true);]
                         |> List.map CreateActor')

    let DrawActor (sb:SpriteBatch) actor =
        if actor.Texture.IsSome then
            do sb.Draw(actor.Texture.Value, actor.Position, Color.White)
        ()
 
    override x.Initialize() =
        ignore spriteBatch         
        do base.Initialize()
        ()
 
    override x.LoadContent() =
        spriteBatch <- new SpriteBatch(x.GraphicsDevice)
        do WorldObjects.Force () |> ignore
        ()
 
    override x.Update (gameTime) =
        ()
 
    override x.Draw (gameTime) =
        do x.GraphicsDevice.Clear Color.CornflowerBlue
        do spriteBatch.Begin()
        do WorldObjects.Force () |> List.iter (DrawActor spriteBatch)
        do spriteBatch.End()
        do base.Draw gameTime
        ()
