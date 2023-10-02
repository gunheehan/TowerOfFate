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
        public char word;
        public int rowIndex;
        public int colIndex;
        public GroupWord RowGroup = null;
        public GroupWord ColGroup = null;
        public WordItemType wordItemType = WordItemType.NONE;
    }

    public class GroupWord
    {
        public string answer;
        public string explanation;
        public int startRow;
        public int startCol;
        public WordItemType wordItemType = WordItemType.NONE;
    }
}
