<script lang="ts">
import type NewsFeed from "../models/NewsFeed.type";
import Article from "./Article.svelte"

export let newsFeed: NewsFeed;
</script>

<style>
.newsFeed {
width: 90vw;
margin-top: 30px;
margin-bottom: 50px;
}
</style>

<div class="newsFeed">
    {#each newsFeed.articles as article}
        <Article article={article}/>
    {/each}
</div>