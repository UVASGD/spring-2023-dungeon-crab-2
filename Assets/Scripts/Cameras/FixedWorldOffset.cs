using UnityEngine;
using Cinemachine;

// This is the script that allows us to offset the player-tracking camera from the camera Dolly that it uses to track the player.
// Normally, the camera would try to find the closest position to the player that exists on the line used by the Dolly and go there.
// With this script, we can offset the camera from that closest position in the inspector in order to customize the camera angle.
[SaveDuringPlay, ExecuteAlways]
[AddComponentMenu("")] // Hide in menu
public class FixedWorldOffset : CinemachineExtension
{
    public Vector3 Offset;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
            state.RawPosition += Offset;
    }
}
