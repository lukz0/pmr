<template>
    <div>
      <label> {{label}} </label>
      <input :value="value" @input="$emit('input',$event.target.value)"/>
    </div>
</template>



<script>
export default {
    props:{
        label: {
            required: false,
            type: String
        },
        value:{
            required: true,
            type: String
        }
    }
}
</script>

<style lang="stylus" scoped>


</style>