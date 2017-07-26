Uses NTwitch version `0.1.2-build-00007`

# Twitchbot
A generic twitch chat bot, provided as an example for the [NTwitch](https://github.com/Aux/NTwitch) library.

![Twitchbot in use](http://i.imgur.com/dChAJIE.png)

#### Example config.yml
```yaml
# A Twitch oauth token the bot will use to log in to chat
token: yourtokenhere

# Channels the bot will join after connecting to chat
channels:
  - auxesistv

# All events the bot should reply to. Replies are
# randomly selected from list provided for each event
#
# Available events: new_sub, re_sub, hosting_started,
# hosting_stopped, user_banned
events:
  new_sub:
    - Thanks for subscribing %user% Kappa
    - Thank you for the sub %user% LUL
  re_sub:
    - Wow! %user% resubscribed for %months% month(s)

# Commands the bot will reply to in chat. Supports basic
# variables like %user% and %channel%.
#
# Available conditions: starts_with, contains, ends_with
commands:
  - starts_with: '!repo'
    replies:
      - https://github.com/Aux/Twitchbot
  - starts_with: '!quote'
    replies:
      - '"I don’t want to earn my living; I want to live." - Oscar Wilde'
      - '"Life shrinks or expands in proportion to one’s courage." - Anais Nin'
      - '"Whatever you are, be a good one." - Abraham Lincoln'
  - contains: thanks twitchbot
    replies:
      - You're welcome %user%!
  - ends_with: ???
    replies:
      - Calm down with the question marks pls
```
