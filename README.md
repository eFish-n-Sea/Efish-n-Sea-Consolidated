# eFish’nSea: Unity Game Set for Learning Software Performance Issues Root Causes and Resolutions

## Purpose
The submitted artifacts include the source code for each of the 8 Unity games. Each of the games is designed to educate young students about specific concepts relating to efficiency. These concepts were originally derived from a software context, but were generalized to convey the ideas to students without any programming knowledge.

Please note that, due to conflicts with the Standard Unity Asset Store EULA, some images could not be included in the artifact submission. As a result, all images have been excluded from the source files of the games. Similarly, the source files for the website, which contain complete builds of each of the games, have also been excluded from this submission. In order to view and play the games with the original images, please visit the website at https://efish-n-sea.github.io/.

The authors are applying for the Available and Reusable badges. All relevant artifacts which have been created by the authors have been placed on a publicly accessible archival repository and are included in this submission. These artifacts include the source code for each of the 8 Unity games. The submission does not include the images used in the website and games, which were not created by the authors. Without these images, the artifacts are still complete and functional. Instructions for preparation and reuse of the artifacts are documented with great care and detail.
## Provenance
The artifacts can be found in the archival repository located at    . A preprint of the paper can be found as a PDF file in this submission.
## Setup
In order to prepare the Unity games for execution, you must first install the Unity development platform. Detailed instructions for this process can be found at https://unity.com/download. The recommended version of Unity is 2021.3.18f1, as this is the latest version on which the games were developed. However, newer versions should also be compatible.

Once Unity is installed, you must then download the source code included in the archival repository. The repository includes 8 folders for each of the 8 games. In the Unity Hub, open one of these folders to open the corresponding game. Each of these 8 folders corresponds to a separate Unity project.

Since images were not included in the artifacts submission, none of the Unity objects will have sprites attached. If you wish to view the objects, you will need to attach sprites to the objects. You may use the default sprites included in Unity, or you may download other images to use. To attach a sprite, first select an object from the “Hierarchy” window. Then, navigate to the “Inspector” window, find the “Sprite Renderer” component, and the “Sprite” element. Click on the circle button to the right of the empty field, and select your desired sprite. Repeat this process for each of the objects you wish to view. Alternatively, the “Color” element of the “Sprite Renderer” component can be changed to differentiate each of the GameObjects without the need to attach sprites.

System requirements for Unity can be found at https://docs.unity3d.com/2021.3/Documentation/Manual/system-requirements.html. Note the version number at the top left, and the differing requirements for the Unity Editor and the Unity Player. The Editor is necessary to open and edit the project files, while the Player is necessary to build and run the games on the website.
## Usage
In order to reproduce the builds of the games created for the results obtained in the paper, the WebGL build settings provided by Unity should be used. Additionally, certain website hosting services, such as GitHub Pages (which is what the authors used for the paper), will fail to load the games properly unless the build settings are modified: particularly, “Compression Format” (under Publishing Settings) needs to be set to “Disabled”.
