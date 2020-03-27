import Vue from 'vue'
import SubmitButton from "./SubmitButton";


[
    SubmitButton
].forEach(c =>{
    Vue.component(c.name, c);
});