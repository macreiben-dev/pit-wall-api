docker build -f ".\Dockerfile" --force-rm -t pitwalldatagatheringapi  --build-arg "BUILD_CONFIGURATION=Debug" --label "com.microsoft.visual-studio.project-name=PitWallDataGatheringApi" "C:\Users\chris\gitrepos\pit-wall-api\src\PitWallDataGatheringApi"