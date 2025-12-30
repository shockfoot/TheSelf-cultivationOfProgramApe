namespace Common
{
    /// <summary>
    /// 单链表结点
    /// </summary>
    public class ListNode<T>
    {
        public T val;
        public ListNode<T> next;
        public ListNode(T val = default(T), ListNode<T> next = null)
        {
            this.val = val;
            this.next = next;
        }

        /// <summary>
        /// 获取单链表长度
        /// </summary>
        public static int GetLength(ListNode<T> head)
        {
            int length = 0;
            while (head != null)
            {
                length++;
                head = head.next;
            }
            return length;
        }
    }
}