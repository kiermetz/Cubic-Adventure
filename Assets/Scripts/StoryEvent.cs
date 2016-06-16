using UnityEngine;
using System.Collections;

public static class StoryEvent {

	private static int introEvent = 0;

	public static int getIntroEvent() {
		return introEvent;
	}

	public static void setIntroEvent(int i) {
		introEvent = i;
		return;
	}

	public static int IncrementIntroEvent() {
		introEvent++;

		return introEvent;
	}

	public static bool EqualsTo(int i) {
		if (i == introEvent)
			return true;
		return false;
	}
}
