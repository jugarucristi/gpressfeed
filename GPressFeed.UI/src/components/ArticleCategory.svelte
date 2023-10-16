<script lang="ts">
  import Category from "../constants/Category";

    export let category: String;

    let color: string;
    switch(category)
    {
        case Category.Entertainment:
            color = "yellow";
            break;
        case Category.Finance:
            color = "green";
            break;
        case Category.Politics:
            color = "red";
            break;
        case Category.Science:
            color = "teal";
            break;
        case Category.Sports:
            color = "orange";
            break;
        case Category.WorldNews:
            color = "#006edd";
            break;
        default:
            color = "grey";
            break;
    }
</script>

<style>
    .category{
        margin-top: 5px;
        margin-bottom: 30px;
        margin-left: 20px;
        font-size: 20px;
    }
</style>

<div class="category" style="color: {color}">
    {#if category != Category.Unknown && category}
        {"[ " + category + " ]"}
    {/if}
</div>
