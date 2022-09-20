using System.Collections;
using UnityCommon;
using UnityEngine;

namespace Shotake
{
    class MainSceneProfileView : GameModeView
    {
        GameModeView m_lastActiveView = null;

        private new void Awake()
        {
            base.Awake();

            UITool.LinkButtonOnClick("ProfileExitButton", OnProfileExitButtonClicked);
            UITool.LinkButtonOnClick("ProfileStatusButton", OnStatusButtonClicked);
            UITool.LinkButtonOnClick("ProfileCardButton", OnCardButtonClicked);
            UITool.LinkButtonOnClick("ProfileResearchButton", OnResearchButtonClicked);
            UITool.LinkButtonOnClick("ProfileShopButton", OnShopButtonClicked);
            UITool.LinkButtonOnClick("ProfileRankButton", OnRankButtonClicked);

            GameModeManager.Instance.DisableMode<ProfileCardView>();
            GameModeManager.Instance.DisableMode<ProfileResearchView>();
            GameModeManager.Instance.DisableMode<ProfileShopView>();
            GameModeManager.Instance.DisableMode<ProfileRankView>();
        }

        public override IEnumerator EnableMode()
        {
            if (m_lastActiveView == null)
            {
                var m = GameModeManager.Instance.GetMode<ProfileStatusView>();
                GameModeManager.Instance.EnableMode(m);
                m_lastActiveView = m;
            }
            else
            {
                GameModeManager.Instance.EnableMode(m_lastActiveView);
            }

            return base.EnableMode();
        }

        public override IEnumerator DisableMode()
        {
            if (m_lastActiveView != null)
            {
                GameModeManager.Instance.DisableMode(m_lastActiveView);
            }

            return base.DisableMode();
        }

        void OnProfileExitButtonClicked()
        {
            var v = GameModeManager.Instance.GetMode<MainSceneEntryView>();
            GameModeManager.Instance.SwitchMode(this, v);
        }

        void OnStatusButtonClicked()
        {
            var v = GameModeManager.Instance.GetMode<ProfileStatusView>();
            if (m_lastActiveView != v)
            {
                GameModeManager.Instance.SwitchMode(m_lastActiveView, v);
                m_lastActiveView = v;
            }
        }

        void OnCardButtonClicked()
        {
            var v = GameModeManager.Instance.GetMode<ProfileCardView>();
            if (m_lastActiveView != v)
            {
                GameModeManager.Instance.SwitchMode(m_lastActiveView, v);
                m_lastActiveView = v;
            }
        }

        void OnResearchButtonClicked()
        {
            var v = GameModeManager.Instance.GetMode<ProfileResearchView>();
            if (m_lastActiveView != v)
            {
                GameModeManager.Instance.SwitchMode(m_lastActiveView, v);
                m_lastActiveView = v;
            }
        }

        void OnShopButtonClicked()
        {
            var v = GameModeManager.Instance.GetMode<ProfileShopView>();
            if (m_lastActiveView != v)
            {
                GameModeManager.Instance.SwitchMode(m_lastActiveView, v);
                m_lastActiveView = v;
            }
        }

        void OnRankButtonClicked()
        {
            var v = GameModeManager.Instance.GetMode<ProfileRankView>();
            if (m_lastActiveView != v)
            {
                GameModeManager.Instance.SwitchMode(m_lastActiveView, v);
                m_lastActiveView = v;
            }
        }
    }
}