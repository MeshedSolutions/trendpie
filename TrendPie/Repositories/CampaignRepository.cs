﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TrendPie.Models;

namespace TrendPie.Repositories
{
    public class CampaignRepository
    {
        public static List<Campaign> GetAll()
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Campaigns.ToList();
            }
        }
        public static List<Campaign> GetAllPending()
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Campaigns.Where(i => i.Status == "Pending").ToList();
            }
        }
        public static List<Campaign> GetAllComplete()
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Campaigns.Where(i => i.Status == "Complete").ToList();
            }
        }
        public static List<Campaign> GetAllActive()
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Campaigns.Where(i => i.Active == "true").ToList();
            }
        }
        public static List<Campaign> GetAllLive()
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Campaigns.Where(i => i.Status == "Live").ToList();
            }
        }
        public static List<Campaign> GetAllActiveLiveForUser(User user)
        {
            using (var db = new TrendPie_Entities())
            {
                List<Campaign> campaigns = (
                    from campaign in db.Campaigns
                    join userCampaign in db.UserCampaigns on campaign.Id equals userCampaign.CampaignID
                    where userCampaign.UserID == user.Id &&
                          campaign.Active == "true" &&
                          campaign.Status == "live"
                    select campaign).ToList();

                return campaigns;
            }
        }
        public static List<Campaign> GetAllActiveCompleteForUser(User user)
        {
            using (var db = new TrendPie_Entities())
            {
                List<Campaign> campaigns = (
                    from campaign in db.Campaigns
                    join userCampaign in db.UserCampaigns on campaign.Id equals userCampaign.CampaignID
                    where userCampaign.UserID == user.Id &&
                          campaign.Active == "true" &&
                          campaign.Status == "complete"
                    select campaign).ToList();

                return campaigns;
            }
        }
        public static List<Campaign> GetAllActivePendingForUser(User user)
        {
            using (var db = new TrendPie_Entities())
            {
                var campaigns = db.Campaigns.Where(i => i.Active == "true" && i.Status == "live").ToList();
                var userCampaigns = db.UserCampaigns.Where(i => i.UserID == user.Id).Select(i => i.CampaignID).ToList();

                List<Campaign> campaignList = campaigns.Where(campaign => !userCampaigns.Contains(campaign.Id)).ToList();

                return campaignList;
            }
        }
        public static List<Campaign> GetTop5ActiveLive()
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Campaigns.Where(i => i.Active == "true" && i.Status == "live").Take(5).ToList();
            }
        }
        public static List<Campaign> GetTop5ActiveComplete()
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Campaigns.Where(i => i.Active == "true" && i.Status == "complete").Take(5).ToList();
            }
        }
        public static List<Campaign> GetTop5ActiveLiveForUser(User user)
        {
            using (var db = new TrendPie_Entities())
            {
                List<Campaign> campaigns = (
                    from campaign in db.Campaigns
                    join userCampaign in db.UserCampaigns on campaign.Id equals userCampaign.CampaignID
                    where userCampaign.UserID == user.Id &&
                          campaign.Active == "true" &&
                          campaign.Status == "live"
                    select campaign).Take(5).ToList();

                return campaigns;
            }
        }
        public static List<Campaign> GetTop5ActiveCompleteForUser(User user)
        {
            using (var db = new TrendPie_Entities())
            {
                List<Campaign> campaigns = (
                    from campaign in db.Campaigns
                    join userCampaign in db.UserCampaigns on campaign.Id equals userCampaign.CampaignID
                    where userCampaign.UserID == user.Id &&
                          campaign.Active == "true" &&
                          campaign.Status == "complete"
                    select campaign).Take(5).ToList();

                return campaigns;
            }
        }
        public static Campaign GetById(int campaignId)
        {
            using (var db = new TrendPie_Entities())
            {
                return db.Campaigns.Find(campaignId);
            }
        }
        public static void Create(Campaign campaign)
        {
            using (var db = new TrendPie_Entities())
            {
                db.Campaigns.Add(campaign);
                db.SaveChanges();
            }
        }
        public static Campaign Update(Campaign campaign)
        {
            using (var db = new TrendPie_Entities())
            {
                db.Entry(campaign).State = EntityState.Modified;
                db.SaveChanges();
            }

            return campaign;
        }
    }
}