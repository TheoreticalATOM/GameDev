using UnityEngine;
public class SwapMaterial : Swapping<Material>
{
	public MeshRenderer mRenderer;

    public override void SetDefault(out Material swapFrom)
    {
		swapFrom = mRenderer.material;
    }

    protected override void OnSwapped(Material swappedTo)
    {
		mRenderer.material = swappedTo;
    }
}
