﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shotake
{
    // 스테이지 세션 플레이어 상태
    // 카드, 레벨, 헤드/바디 ...
    internal class SK_PlayerState : PlayerState
    {
        public float m_hp;
        public float m_exp;
        public float m_expMax;

        public float Exp
        {
            get => m_exp;
        }

        public float MaxExp
        {
            get => m_expMax;
        }

        public float HP
        {
            get => m_hp;
            set
            {
                m_hp = value;
                m_hp = MathF.Max(m_hp, 0);
            }
        }
    }
}
