From mcr.microsoft.com/dotnet/sdk:6.0


# Install common tools
# IF YOU WANT TO DECREASE IMAGE'S SIZE, REMOVE THE UNNECESSARY TOOLS FROM THIS COMMAND
# Use a backslash (\), if you want to break up a command to more than one line.
# The command "rm -rf /var/lib/apt/lists/*" removes redundant files from a given layer

RUN apt-get update \
    && apt-get -y install curl \
    && apt-get -y install git \	
    && apt-get -y install wget \	
    && apt-get -y install net-tools \
    && apt-get -y install zip \
    && rm -rf /var/lib/apt/lists/*


# Install packages required by Chrome.
# If you want to install a newer Chrome version which requires a few more packages (but you don't know which), you will see the proper output.

RUN apt-get update && apt-get install -y lsb-release libgtk-3-0 libappindicator3-1 xdg-utils libxss1 libnss3 libnspr4 libasound2 libappindicator1 fonts-liberation libxss1 xdg-utils libasound2 libpango1.0-0 libpangoxft-1.0-0 libv4l-0 libv4lconvert0



RUN wget -q https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
##RUN wget -q https://dl.google.com/linux/deb/pool/main/g/google-chrome-stable/google-chrome-stable_108.0.5359.71_amd64.deb

RUN yes |apt-get install ./google-chrome-stable_current_amd64.deb

RUN yes|apt --fix-broken install

RUN set -x \
&& apt-get update \
&& apt-get install -y --no-install-recommends \
ca-certificates \
curl \
unzip \
&& rm -rf /var/lib/apt/lists/* \
&& curl -sSL "https://dl.google.com/linux/direct/google-talkplugin_current_amd64.deb" -o /tmp/google-talkplugin-amd64.deb \
&& dpkg -i /tmp/google-talkplugin-amd64.deb \
&& curl -sSL "https://chromedriver.storage.googleapis.com/2.40/chromedriver_linux64.zip" -o /tmp/chromedriver.zip \
&& unzip -o /tmp/chromedriver -d /usr/bin/ \
&& rm -rf /tmp/*.deb \
&& apt-get purge -y --auto-remove curl unzip


WORKDIR /src

COPY . .

RUN dotnet build

