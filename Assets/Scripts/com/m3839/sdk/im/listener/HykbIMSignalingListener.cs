using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.m3839.sdk.im.listener
{
    public abstract class HykbIMSignalingListener : AndroidJavaProxy
    {
        public HykbIMSignalingListener() : base("com.m3839.sdk.im.listener.HykbIMSignalingListener") { }

        public void onReceiveNewInvitation(string inviteID, string inviter, string groupID, AndroidJavaObject inviteeList, string data)
        {
            List<string> dataList = new List<string>();

            if (inviteeList != null)
            {
                int size = inviteeList.Call<int>("size"); //list
                for (int i = 0; i < size; i++)
                {
                    dataList.Add(inviteeList.Call<string>("get", i));
                }
            }
            OnReceiveNewInvitation(inviteID, inviter, groupID, dataList, data);
        }

        public void onInviteeAccepted(string inviteID, string invitee, string data)
        {
            OnInviteeAccepted(inviteID, invitee, data);
        }

        public void onInviteeRejected(string inviteID, string invitee, string data)
        {
            OnInviteeRejected(inviteID, invitee, data);
        }

        public void onInvitationCancelled(string inviteID, string inviter, string data)
        {
            OnInvitationCancelled(inviteID, inviter, data);
        }

        public void onInvitationTimeout(string inviteID, AndroidJavaObject inviteeList)
        {
            List<string> dataList = new List<string>();

            if (inviteeList != null)
            {
                int size = inviteeList.Call<int>("size"); //list
                for (int i = 0; i < size; i++)
                {
                    dataList.Add(inviteeList.Call<string>("get", i));
                }
            }
            OnInvitationTimeout(inviteID, dataList);
        }

        public abstract void OnReceiveNewInvitation(string inviteID, string inviter, string groupID, List<string> inviteeList, string data);

        public abstract void OnInviteeAccepted(string inviteID, string invitee, string data);

        public abstract void OnInviteeRejected(string inviteID, string invitee, string data);

        public abstract void OnInvitationCancelled(string inviteID, string inviter, string data);

        public abstract void OnInvitationTimeout(string inviteID, List<string> inviteeList);
    }
}

