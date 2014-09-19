using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTasks.Pubnub
{
    public class NotificationCreator
    {
        private PubnubAPI pubnub;
        private static NotificationCreator creator;
        private string channel;

        private NotificationCreator()
        {
            this.pubnub = new PubnubAPI(
                "pub-c-64796d9d-5e70-4409-b54d-44a0351fee2e",               // PUBLISH_KEY
                "sub-c-b55bc4f6-3d9c-11e4-8e82-02ee2ddab7fe",               // SUBSCRIBE_KEY
                "sec-c-ZDZlZGFhYmUtYjdmOS00MDQ3LWFiYjYtNTYyY2Y1YmRkZmJl",   // SECRET_KEY
			    true                                                        // SSL_ON?
		    );

            this.channel = "TodoNotification";
        }

        public static NotificationCreator Instance
        {
            get
            {
                if (creator == null)
                {
                    creator = new NotificationCreator();
                }

                return creator;
            }
        }

        public void AddTaskNotification(string content, DateTime added, string name)
        {
            var message = String.Format("User {0} added new task {1} on {2}", name, content, added);
            pubnub.Publish(channel, message);
        }

        public void ChangeTaskNotification(string content, string newContent, string name)
        {
            var message = String.Format("User {0} changed task {1} to {2}",name, content, newContent);
            pubnub.Publish(channel, message);
        }

        public void DeleteTaskNotification(string content, string name)
        {
            var message = String.Format("Task {0} was deleted by {1}", content, name);
            pubnub.Publish(channel, message);
        }
    }
}
