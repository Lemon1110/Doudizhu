using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

/// <summary>
/// 游戏结束View
/// </summary>
public class GameOverView :EventView{
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; } //全局的
	public void OnRestartClick()
    {
        dispatcher.Dispatch(ViewEvent.RESTART_GAME);
        Destroy(gameObject);
    }
    public void OnExitClick()
    {
        Application.Quit();
    }
	
}
