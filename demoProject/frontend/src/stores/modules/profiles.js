
export default {
    namespaced: true,

    state:{
        profiles:[]

    },
    getters:{
        GET_PROFILE: state => name => state.profiles.filter(profile => profile.firstName === name)[0]
    },
    mutations:{
        SET_PROFILES(state, profiles){
            state.profiles = profiles;
        },
        ADD_PROFILE(state, profile){
            state.profiles.push(profile);
        }
    },
    actions:{
        LOAD_PROFILES({commit}, api){
            api.get("Profile")
                .then(res => {
                    commit("SET_PROFILES", res.data);
                })
        },
        CREATE_PROFILE({commit, dispatch}, payload){
            let {api, form} = payload;
            api.post("Profile", form).then(res => {
                    commit("ADD_PROFILE", res.data);
                    dispatch("popup/DISPLAY_POPUP", "Profile Created",{root: true})
                });
        }
    }
}
