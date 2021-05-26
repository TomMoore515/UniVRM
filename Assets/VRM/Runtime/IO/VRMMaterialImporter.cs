﻿using System;
using System.Collections.Generic;
using UniGLTF;
using UnityEngine;
using VRMShaders;

namespace VRM
{
    public sealed class VRMMaterialImporter : IMaterialImporter
    {
        readonly glTF_VRM_extensions m_vrm;
        public VRMMaterialImporter(glTF_VRM_extensions vrm)
        {
            m_vrm = vrm;
        }

        public MaterialImportParam GetMaterialParam(GltfParser parser, int i)
        {
            // mtoon
            if (!VRMMToonMaterialImporter.TryCreateParam(parser, m_vrm, i, out MaterialImportParam param))
            {
                // unlit
                if (!GltfUnlitMaterialImporter.TryCreateParam(parser, i, out param))
                {
                    // pbr
                    if (!GltfPbrMaterialImporter.TryCreateParam(parser, i, out param))
                    {
                        // fallback
#if VRM_DEVELOP
                        Debug.LogWarning($"material: {i} out of range. fallback");
#endif
                        return new MaterialImportParam(GltfMaterialImporter.GetMaterialName(i, null), GltfPbrMaterialImporter.ShaderName);
                    }
                }
            }
            return param;
        }
    }
}
