<script lang="ts">
import type NewsFeed from "../models/NewsFeed.type";
import ArticleLink from "./ArticleLink.svelte"

export let newsFeed: NewsFeed;
</script>

<style>
.newsFeedList {
width: 90vw;
margin-top: 30px;
margin-bottom: 50px;
}
@media screen and (max-width: 480px){
    .newsFeedList {
    margin-top: 15px;
    margin-bottom: 50px;
    }
}
</style>

<div class="newsFeedList">
    {#each newsFeed.articles as article}
        <ArticleLink article={article}/>
    {/each}
</div>