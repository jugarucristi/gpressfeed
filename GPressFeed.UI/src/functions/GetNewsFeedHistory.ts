import type NewsFeed from "../models/NewsFeed.type";
import GetGPressFeedApiUrl from "./GetGPressFeedApiUrl";

export default async function GetNewsFeedHistory(
  numberOfFeeds: number
): Promise<NewsFeed[]> {
  try {
    const requestSettings = {
      method: "GET",
      headers: {
        Accept: "application/json",
      },
    };
    const response = await fetch(
      GetGPressFeedApiUrl() +
        "/api/pressfeed/history?numberoffeeds=" +
        numberOfFeeds,
      requestSettings
    );

    if (!response.ok) throw new Error(`Error! status: ${response.status}`);

    const result = (await response.json()) as NewsFeed[];

    return result.splice(1, 10);
  } catch {
    throw new Error("Api is down!");
  }
}
