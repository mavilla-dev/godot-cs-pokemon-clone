using System;
using Godot;

public class UiManager {
  private readonly CanvasLayer _root;

  public UiManager(CanvasLayer root) {
    _root = root;
  }

  public void ClearActiveUi() {
    foreach (var child in _root.GetChildren()) {
      _root.RemoveChild(child);
      child.QueueFree();
    }
  }

  public T SwapUi<T>(Resource resource) where T : Node {
    ClearActiveUi();

    var packedScene = GD.Load<PackedScene>(resource.ResourcePath);
    var instance = packedScene.Instantiate<T>();
    _root.AddChild(instance);
    return instance;
  }

  internal T AddUi<T>(Resource resource) where T : Node {
    var packedScene = GD.Load<PackedScene>(resource.ResourcePath);
    var instance = packedScene.Instantiate<T>();
    _root.AddChild(instance);

    return instance;
  }
}
