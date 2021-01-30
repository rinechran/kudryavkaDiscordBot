FROM ubuntu:20.10

RUN apt update && \
 apt install -y wget

# 20.04 apt package add
RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
sudo dpkg -i packages-microsoft-prod.deb

# .NET Core runtime Install
RUN apt-get update; \
  apt-get install -y apt-transport-https && \
  apt-get update && \
  apt-get install -y aspnetcore-runtime-3.1

CMD ["kudryavkaDiscordBot.exe"]
