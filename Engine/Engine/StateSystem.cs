using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class StateSystem
    {
        /// <summary>
        /// 字典
        /// </summary>
        Dictionary<string, IGameObject> _stateStore = new Dictionary<string, IGameObject>();
        IGameObject _currentState = null;
        /// <summary>
        /// 更新游戏状态界面
        /// </summary>
        /// <param name="elapsedTime">时间间隔</param>
        public void Update(double elapsedTime)
        {
            if (_currentState == null)
            {
                return; // nothing to process
            }
            _currentState.Update(elapsedTime);
        }
        /// <summary>
        /// 渲染游戏状态界面
        /// </summary>
        public void Render()
        {
            if (_currentState == null)
            {
                return; // nothing to render
            }
            _currentState.Render();
        }
        /// <summary>
        /// 添加游戏状态
        /// </summary>
        /// <param name="stateId">状态界面名称</param>
        /// <param name="state">状态值</param>
        public void AddState(string stateId, IGameObject state)
        {
            System.Diagnostics.Debug.Assert(Exists(stateId) == false);
            _stateStore.Add(stateId, state);
        }
        /// <summary>
        /// 切换游戏状态界面
        /// </summary>
        /// <param name="stateId">状态界面名称</param>
        public void ChangeState(string stateId)
        {
            System.Diagnostics.Debug.Assert(Exists(stateId));
            _currentState = _stateStore[stateId];
        }

        /// <summary>
        /// 确认状态界面存在
        /// </summary>
        /// <param name="stateId">要确认的状态名称</param>
        /// <returns>存在为True</returns>
        public bool Exists(string stateId)
        {
            return _stateStore.ContainsKey(stateId);
        }
    }
}
