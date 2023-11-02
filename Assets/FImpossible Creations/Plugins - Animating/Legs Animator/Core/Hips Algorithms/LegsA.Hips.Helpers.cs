﻿using FIMSpace.FTools;
using System;
using UnityEngine;

namespace FIMSpace.FProceduralAnimation
{
    public partial class LegsAnimator
    {

        bool _updateHipsAdjustements = true;
        protected void Hips_PreCalibrate()
        {
            if (HipsAdjustingBlend * _MainBlendPlusGrounded <= 0f) { _updateHipsAdjustements = false; }
            _updateHipsAdjustements = true;

            /*if (Calibrate) */ // Hips should be always precalibrated!
            HipsSetup.PreCalibrate();

            Hips_Calc_PreRefreshVariables();

            HipsHubs_PreCalibrate();
        }

        void Hips_Calc_Elasticity()
        {
            if (HipsSetup.HipsElasticityBlend > 0f)
            {
                Vector3 offsetPos = HipsSetup.HipsMuscle.Update(DeltaTime, _Hips_StabilityLocalOffset);
                
                if (offsetPos.y > 0f)
                {
                    offsetPos.y *= 1f-ImpulsesDampUpPushes;
                }

                if (HipsSetup.HipsElasticityBlend < 1f)
                {
                    _Hips_FinalStabilityOffset = Vector3.LerpUnclamped(_Hips_StabilityLocalOffset, offsetPos, HipsSetup.HipsElasticityBlend);
                }
                else
                {
                    _Hips_FinalStabilityOffset = offsetPos;
                }
            }
            else
            {
                _Hips_FinalStabilityOffset = _Hips_StabilityLocalOffset;
            }

            _Hips_FinalStabilityOffset = RootToWorldSpaceVec(_Hips_FinalStabilityOffset);
        }

    }
}