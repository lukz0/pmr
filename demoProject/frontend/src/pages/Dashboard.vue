<template>
    <div>
        <div class="sidenav">
            <router-link to="/missions">Missions</router-link>
            <router-link to="/registers">Registers</router-link>
            <router-link to="/stats">Stats</router-link>
            <router-link to="/settings">Settings</router-link>
        </div>
        <div class="content"><router-view></router-view></div>
    </div>

</template>



<script>
    export default {
        created() {
            console.log("Dashboard Reached")
        }
    }
</script>

<style lang="stylus" scoped>
    ul
        list-style-type: none;
        margin: 0;
        padding: 0;
        width: 200px;
        background-color: #f1f1f1;


    li a
        display: block;
        color: #000;
        padding: 8px 16px;
        text-decoration: none;


    li a.active
        background-color: #4CAF50;
        color: white;


    li a:hover:not(.active)
        background-color: #555;
        color: white;



    .sidenav
        height: 100%;
        width: 160px;
        position page;
        float left
        z-index: 1;
        top: 0;
        left: 0;
        background-color: #111;
        overflow-x: hidden;
        padding-top: 20px;


    .sidenav a
        padding: 6px 8px 6px 16px;
        text-decoration: none;
        font-size: 25px;
        color: #818181;
        display: block;

    .sidenav a:hover
        color: #f1f1f1;

    .main
        margin-left: 160px; /* Same as the width of the sidenav */
        font-size: 28px; /* Increased text to enable scrolling */
        padding: 0px 10px;
    @media screen and (max-height: 450px)
        .sidenav {padding-top: 15px;}
        .sidenav a {font-size: 18px;}

    .content
        background-color lawngreen;
        padding-top: 20px;



</style>