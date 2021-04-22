using System;
using Stardust.Flux.Contract.DTO.Schedule;
using Stardust.Flux.ScheduleEngine.Models;
using Newtonsoft.Json.Linq;

namespace Stardust.Flux.ScheduleEngine.Factory
{
    public static class EventJobFactory
    {

        public static Event CreateEntity<TParam>(BaseEventDto request, TParam extraParams)
        {
            return new Event
            {
                EventId = Guid.NewGuid().ToString(),
                Duration = TimeSpan.FromSeconds(request.DurationSeconds),
                ExtraParams = extraParams,
                ParamType = typeof(TParam).FullName,
                Name = request.Name,                
            };
        }


        public static Event CreateEntity<TParam>(ScheduleEventDto<TParam> scheduleeventRequest)
        {
            var job = CreateEntity(scheduleeventRequest, scheduleeventRequest.ExtraParams);
            job.EventType = EventType.Schedule;
            job.ScheduleAt = scheduleeventRequest.ScheduleAt;
            return job;
        }


        public static Event CreateEntity<TParam>(RecuringEventDto<TParam> scheduleeventRequest)
        {
            var job = CreateEntity(scheduleeventRequest, scheduleeventRequest.ExtraParams);
            job.EventType = EventType.Recuring;
            job.CronExpression = scheduleeventRequest.CronExpression;
            job.StartRecordJobId = Guid.NewGuid().ToString();
            return job;
        }

        public static void UpdateEntity<TParam>(Event recordJob, ScheduleEventDto<TParam> eventRequest)
        {
            UpdateEntity(recordJob, (BaseEventDto)eventRequest, eventRequest.ExtraParams);
            if (eventRequest.ScheduleAt > DateTime.UtcNow)
            {
                recordJob.ScheduleAt = recordJob.ScheduleAt;
            }
        }


        public static void UpdateEntity<TParam>(Event recordJob, BaseEventDto eventRequest, TParam extraParams)
        {
            recordJob.Name = eventRequest.Name;
            recordJob.Duration = TimeSpan.FromSeconds(eventRequest.DurationSeconds);
            recordJob.ExtraParams = extraParams;
        }




        public static ScheduleEventResponse<TParam> CreateScheduleResponseDto<TParam>(Event recordJob)
        {
            var schedule = new ScheduleEventDto<TParam>
            {
                Id = recordJob.EventId,
                DurationSeconds = recordJob.Duration.TotalSeconds,
                ExtraParams = ((JObject)recordJob.ExtraParams).ToObject<TParam>(),
                Name = recordJob.Name,
                ScheduleAt = recordJob.ScheduleAt ?? DateTime.MinValue
            };
            return new ScheduleEventResponse<TParam>(recordJob.StartRecordJobId, recordJob.StopRecordJobId, schedule);
           
        }

        public static void UpdateEntity<TParam>(Event recordJob, RecuringEventDto<TParam> eventRequest)
        {
            UpdateEntity(recordJob, (BaseEventDto)eventRequest, eventRequest.ExtraParams);
            recordJob.CronExpression = recordJob.CronExpression;
        }

        public static RecuringEventResponse<TParam> CreateRecuringResponseDto<TParam>(Event recordJob)
        {
            var recuring = new RecuringEventDto<TParam>
            {
                Id = recordJob.EventId,
                DurationSeconds = recordJob.Duration.TotalSeconds,
                Name = recordJob.Name,
                CronExpression = recordJob.CronExpression,
                ExtraParams = (TParam)recordJob.ExtraParams

            };
            return new RecuringEventResponse<TParam>
            (
                recordJob.StartRecordJobId,
                recordJob.StopRecordJobId,
                recuring
            );
        }
    }
}