<template>
    <button @click="$emit('click')">
        <span v-if="loading">Loading...</span>
        <slot v-else></slot>
    </button>
    
</template>

<script>
    export default {
        name: "a-sbutton",
        props:{
            loading:{
                required: false,
                type: Boolean
            }
        }
    }
</script>

<style lang="stylus" scoped>
button
    color: white;
    background #a38ee9
    border 1px solid #000
    padding 0.5rem 1rem
    border none
    box-shadow 0 0 2px 1px #a38ee9
    transition ease-in 150ms
    &:focus
        outline none
    &:hover
        background lawngreen
        cursor pointer





</style>