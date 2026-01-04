using Common;

namespace LeetCode.LC0001至LC0100;

public class LC0002两数相加
{
    private ListNode<int> AddTwoNumbers(ListNode<int> l1, ListNode<int> l2, byte way = 1)
    {
        switch (way)
        {
            case 1:
                return AddTwoNumbers1(l1, l2);
            case 2:
                return AddTwoNumbers2(l1, l2);
            default:
                throw new ArgumentOutOfRangeException(nameof(way), way, null);
        }
    }

    private ListNode<int> AddTwoNumbers1(ListNode<int> l1, ListNode<int> l2)
    {
        int sum = 0;
        // 哨兵
        ListNode<int> dummy = new ListNode<int>(), cur = dummy;
        while (l1 != null || l2 != null || sum > 0)
        {
            if (l1 != null)
            {
                sum += l1.val;
            }
            if (l2 != null)
            {
                sum += l2.val;
            }
            cur.next = new ListNode<int>(sum % 10);
            cur = cur.next;
            sum /= 10;
            if (l1 != null)
            {
                l1 = l1.next;
            }
            if (l2 != null)
            {
                l2 = l2.next;
            }
        }

        return dummy.next;
    }

    private ListNode<int> AddTwoNumbers2(ListNode<int> l1, ListNode<int> l2)
    {
        return AddTwoNumbers(l1, l2, 0);
    }

    private ListNode<int> AddTwoNumbers(ListNode<int> l1, ListNode<int> l2, int carry)
    {
        // 两个链表都为空时结束递归
        if (l1 == null && l2 == null)
        {
            // 判断是否仍需要进位
            return carry > 0 ? new ListNode<int>(carry) : null;
        }

        // 当l1为null时l2必不为null，交换两个表，简化代码
        if (l1 == null)
        {
            l1 = l2;
            l2 = null;
        }

        // 处理当前值并向下一位传递
        carry += l1.val + (l2 == null ? 0 : l2.val);
        l1.val = carry % 10;
        l1.next = AddTwoNumbers(l1.next, l2 == null ? null : l2.next, carry / 10);

        return l1;
    }
}