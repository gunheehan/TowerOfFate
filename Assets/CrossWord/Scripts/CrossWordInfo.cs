
using System.Collections.Generic;
public enum WordItemType
{
    NONE,
    ROW,
    COL,
    CROSS
}

public class CrossWordInfo
{
    public class SingleWord
    {
        public List<string> answerKeyList = null;
        public char word;
        public int rowIndex;
        public int colIndex;
        public WordItemType wordItemType = WordItemType.NONE;
    }

    public class GroupWord
    {
        public string answer;
        public string explanation;
        public List<SingleWord> singleWordGroup;
        public int startRow;
        public int startCol;
    }
}
