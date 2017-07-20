# TwitchSelfbot
A bot that auto reacts to events with configured replies in twitch chat.

#### Example config.yml
```yaml
# A Twitch oauth token the bot will use to log in to chat
token: oi1j2o3i

# Channels the bot will join after connecting to chat
channels:
  - auxesistv
  - shiralya

# All events the bot should reply to. Replies are
# randomly selected from list provided for each event
#
# Available events: new_sub, re_sub, hosting_started, 
# hosting_ended, hosted_started, hosted_ended, user_banned
#
# Available variables: %user%, %channel%
events:
  new_sub:
    - Thanks for subscribing %user% Kappa
    - Thank you for the sub %user% LUL
  re_sub:
    - Wow! %user% resubscribed for %months% month(s)
```
