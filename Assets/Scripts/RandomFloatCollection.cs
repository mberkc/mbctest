/*
 * 
 * A persistent type of asset, not living in any scene, is needed for a system.
 * It needs to be created inside the editor in the right-click context menu ("Create/Random/FloatCollection").
 * Item's inspector should show an array of floats, just like a regular behaviour's inspector.
 * There should be a button in the inspector that says "Generate" and it should populate the array shown, with random values between 0 and 1.
 * The generated values should persist between editor sessions, scene loads, etc. So make sure of that.
 * 
 * 
 * Code from UnityEditor namespace is not allowed in the build, but RandomFloatCollection class should make it to the build. 
 * Use preprocessor definitions to handle that. What would you do if for some reason you weren't allowed to use preprocessor definitions?
 * 
 * Continue implementing the class however you wish.
 * 
 * 
 */
// Yazacağım class sayesinde Editorde Create/LevelObject gibi Create/Random/FloatCollection tuşu ekleyeceğim(asset folderda). 
// Eklenen item seçildiğinde inspectorda float array ve generate button yer alacak. Bu buttona basıldığında arraydeki floatlar 
// 0-1 arasında random oluşturulacak ve bu item/array kalıcı olacak. 

using UnityEngine;

[CreateAssetMenu (fileName = "NewFloatCollection", menuName = "Random/FloatCollection", order = 0)]
public class RandomFloatCollection : ScriptableObject {
    public float[] floatCollection = new float[10]; // Default Size

    public void Generate () {
        for (int i = 0; i < floatCollection.Length; i++)
            floatCollection[i] = UnityEngine.Random.Range (0f, 1f);
    }

}