using System.Collections.Generic;
public class RecordData : TableBase
{
	public int skillID;
	public List<EventValue> playerEventList;
	public List<MouseEventValue> mouseEventList;
	public List<MonsterEventValue> monsterEvenetList;
	//public List<>
	public override int GetTableID()
	{
		return skillID;
	}
	public RecordData()
	{

	}
}
