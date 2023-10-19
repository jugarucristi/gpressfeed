import type NewsFeed from "../models/NewsFeed.type";
import GetGPressFeedApiUrl from "./GetGPressFeedApiUrl";

export default async function GetNewsFeedById(
  feedId: string
): Promise<NewsFeed> {
  try {
    const requestSettings = {
      method: "GET",
      headers: {
        Accept: "application/json",
      },
    };
    const response = await fetch(
      GetGPressFeedApiUrl() + "/api/pressfeed?feedid=" + feedId,
      requestSettings
    );

    if (!response.ok) throw new Error(`Error! status: ${response.status}`);

    const result = (await response.json()) as NewsFeed;

    return result;
  } catch {
    throw new Error("Api is down!");
  }
}
