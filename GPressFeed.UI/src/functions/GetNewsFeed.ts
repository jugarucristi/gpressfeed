import type NewsFeed from "../models/NewsFeed.type";

export default async function GetNewsFeed(): Promise<NewsFeed>
{
    try {
        const requestSettings = {
            method: 'GET',
            headers: {
                Accept: 'application/json',
            }
        };
        const response = await fetch('http://localhost/api/pressfeed/', requestSettings)

        if(!response.ok)
            throw new Error(`Error! status: ${response.status}`);

        const result = (await response.json()) as NewsFeed;

        return result;
    }
    catch {
        throw new Error('Api is down!');
    }
}