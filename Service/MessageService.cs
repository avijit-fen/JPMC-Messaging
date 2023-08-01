using Messaging.POCO;
using Messaging.ServiceHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Service
{
    public class MessageService : IMessageService
    {
        private IMessageRepositroy _repository;
        
        public MessageService(IMessageRepositroy messageRepository) {

            _repository = messageRepository;
            
        }

        public Message Add(Message msgobject)
        {
            _repository.Add(msgobject);
            return msgobject;
        }

        public async Task<Message> AddAsync(Message msgobject)
        {
           return await Task.Run(()=> { _repository.Add(msgobject); return msgobject; });
        }

        public List<Message> GetAll() {
            
            return _repository.GetAll() ; 
        }

        public void Update(Message item)
        {
            _repository.Update(item);
        }
    }
}
