using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


//�L�[�ۑ��f�[�^�p�̃N���X
[System.Serializable]
public class InputData
{
    //�ݒ�o����L�[
    public KeyCode Move_Forward = KeyCode.W;            //�O�ֈړ�����L�[
    public KeyCode Move_Back = KeyCode.S;               //���ֈړ�����L�[
    public KeyCode Move_Left = KeyCode.A;               //���ֈړ�����L�[
    public KeyCode Move_Right = KeyCode.D;              //�E�ֈړ�����L�[
    public KeyCode Chara_Fire = KeyCode.Mouse0;         //�e���ˌ�����L�[
    public KeyCode Chara_Grenade = KeyCode.G;           //�O���l�[�h�𓊂���L�[
    public KeyCode Chara_Reload = KeyCode.R;            //�����[�h����L�[
    public KeyCode Chara_Interact = KeyCode.E;          //�I�u�W�F�N�g�ɃC���^���N�g����L�[
    public KeyCode Chara_PickModeSwitch = KeyCode.V;    //�s�b�N���[�h���`�F���W����L�[
    public KeyCode Game_Map = KeyCode.M;                //�S�̃}�b�v��\������L�[
}
