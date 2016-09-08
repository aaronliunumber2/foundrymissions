using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoundryMissionsCom.Models.FoundryMissionModels;
using System.Text.RegularExpressions;

namespace FoundryMissionsCom.Helpers
{
    public static class MissionVideosHelper
    {
        private const string YoutubeRegex = @"(?:https?:\/\/)?(?:www\.)?youtu\.?be(?:\.com)?\/?.*(?:watch|embed)?(?:.*v=|v\/|\/)([\w\-_]+)\&?";

        public static List<string> GetVideoLinks(List<string> videos)
        {
            var videoIds = new List<string>();
            foreach (var videolink in videos)
            {
                Match youtube = Regex.Match(videolink, YoutubeRegex);

                if (youtube.Success)
                {
                    videoIds.Add(youtube.Groups[1].Value);
                }
            }

            return videoIds;
        }

        internal static void AddVideos(List<string> videos, Mission mission)
        {
            if (mission.Videos == null)
            {
                mission.Videos = new List<YoutubeVideo>();
            }

            foreach (var videolink in videos)
            {
                mission.Videos.Add(new YoutubeVideo() { MissionId = mission.Id, Order = mission.Videos.Count, YoutubeVideoId = videolink });
            }
        }
    }
}