using System;

namespace Shotake
{
    /// <summary>
    /// sessionState
    /// </summary>
    class SK_PlayerState : PlayerState
    {
        public float m_hp;
        public float m_exp;

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
