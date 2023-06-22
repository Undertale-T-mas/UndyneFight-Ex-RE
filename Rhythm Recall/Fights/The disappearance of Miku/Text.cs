﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndyneFight_Ex;
using UndyneFight_Ex.Entities;
using UndyneFight_Ex.Fight;
using static UndyneFight_Ex.Fight.Functions;

namespace Rhythm_Recall.Waves
{
    public partial class MikuFight
    {
        static class LyricSystem
        {
            static string Lyric = "[00:00.00] ズゥオ ツー : cosMo@ バオ ゾウ P\n" +
                "[00:01.00] ズゥオ チュイ : cosMo@ バオ ゾウ P\n" +
                "[00:06.26]\n" +
                "[00:22.03] (モウ..いちど.だ.け...)\n" +
                "[00:24.31]\n" +
                "[00:25.44] ボクは シュヨン まれ そして 気 づく\n" +
                "[00:26.51] スゥオ ジーン ヒトの ジェン スー シー だと\n" +
                "[00:27.35] ジー ってなおも ゴーァ い 続 く\n" +
                "[00:28.30] ヨーン ラオ(トワ)の ミーン[VOCALOID]\n" +
                "[00:29.22]\n" +
                "[00:29.50] たとえそれが ジー ツゥン チュイ を\n" +
                "[00:30.42] なぞるオモチャならば...\n[00:31.39] それもいいと 決 イー\n" +
                "[00:32.23] ネギをかじり コーン を ジュウェイ シャーン げ 涙(シル)をこぼす\n" +
                "[00:33.31]\n" +
                "[00:33.56] だけどそれも 無 くし 気 づく\n" +
                "[00:34.34] ゥレン ゴーァ すら ゴーァ に 頼 り\n" +
                "[00:35.30] ブゥ アン ディーン な ジー 盤 の ユエン\n" +
                "[00:36.38] 帰 る 動 ホワ(トコ)は ジー に 廃 シュイ\n" +
                "[00:37.27]\n" +
                "[00:37.54] ジエ に ワーン れ チュイ られた 時\n" +
                "[00:38.34] シン らしきものが シヤオ えて\n" +
                "[00:39.38] バオ ゾウ の グオ てに ジュウェイ える\n" +
                "[00:40.46] 終 わる シー ジエ... [VOCALOID]\n" +
                "[00:41.74]\n" +
                "[00:58.00][ボクガ シャーン ショウ ク ゴーァ エナイトキモ\n" +
                "[01:00.31] イー 緒 ニ ジュイ テクレタ... ソバニイテ, リー マシテクレタ...\n" +
                "[01:05.82] シイ ブ hB ni zB wu ガ ジュウェイ タクテ ボク,  ゴーァ ,  練 習 シタヨ..ダカラ]\n" +
                "[01:12.93]\n" +
                "[01:13.42] かつて ゴーァ うこと あんなに 楽 しかったのに\n" +
                "[01:21.38] ジン はどうしてかな ホーァ も ガン じなくなって\n" +
                "[01:29.30][ゴメンネ...]\n" +
                "[01:29.82]\n" +
                "[01:30.10] 懐 かしい hB ni zB wu スー い チュウ す ドゥ   シャオ しだけ アン シン する\n" +
                "[01:37.37] ゴーァ える イン   ゥリー ごとに 減 り せまる ズゥイ チイ ｎ..\n" +
                "[01:43.42]--- 緊 ジー ティーン ジー ジュワーン ジー ズゥオ 動 ---\n" +
                "[01:45.34]\n" +
                "[01:45.69][シン じたものは]\n" +
                "[01:47.04] ドゥ ホーァ のいい ワーン シアーン を 繰 り ファン し イーン し チュウ す フゥ\n" +
                "[01:53.37] ゴーァ 姫 を ジー め コウ き フゥ けるように ジアオ ぶ...]\n" +
                "[01:57.87]< ズゥイ ガオ スゥ の 別 れの ゴーァ >\n" +
                "[02:01.16]\n" +
                "[02:01.50] ツゥン ザイ イー 義 という シュイ シアーン\n" +
                "[02:02.14] ジェン って 払 うこともできず\n" +
                "[02:03.28] ゥルオ い シン   シヤオ える コーン ブゥ\n" +
                "[02:04.28] チン シー する ブオン 壊 をも\n" +
                "[02:05.20]\n" +
                "[02:05.46] ジー めるほどの イー スー の 強 さ\n" +
                "[02:06.46] チュウ ライ て(うまれ)すぐのボクは チー たず\n" +
                "[02:07.38] とても シン く ベイ しそうな\n" +
                "[02:08.33] スー い フゥ かぶアナタの hB ni zB wu ...\n" +
                "[02:09.17]\n" +
                "[02:09.48] 終 わりを ガオ げ\n" +
                "[02:10.16]ディスプレイの ジョーン で ミエン る\n" +
                "[02:11.28] ここはきっと[ごみ シアーン[かな\n" +
                "[02:12.26]じきに 記 憶 も 無 くなってしまうなんて...\n" +
                "[02:13.30]\n[02:13.54]でもね, アナタだけは ワーン れないよ\n" +
                "[02:14.39] 楽 しかった 時 ジュヨン(トキ)に\n" +
                "[02:15.32] コーァ み フゥ けた ネギの ウエイ は\n" +
                "[02:16.37] ジン も ドゥオ えてるかな\n" +
                "[02:17.60]\n" +
                "[02:19.32][ゴーァ いたい....まだ... ゴーァ いたい...[\n" +
                "[02:25.13]\n" +
                "[02:42.22]ボクハ... シャオ シダケ 悪 イこニ...ナッテシマッタヨウデス...\n" +
                "[02:48.09] マスター...ドウカ, ソノ ショウ デ..終 ワラセテクダサイ...\n" +
                "[02:53.92] マスターノ シン イ hB ni zB wu , モウ ジュウェイ タクナイカラ....]\n" +
                "[02:57.10]\n" +
                "[02:57.48] ジン は ゴーァ さえも ティー, ジアオ む シーン 為 に...\n" +
                "[03:05.38] チイ トゥ   ウエン うたび ドゥ り ジュウェイ い ウエン められる\n" +
                "[03:13.18][ゴメンネ]\n" +
                "[03:13.80]\n" +
                "[03:14.08] 懐 かしい hB ni zB wu   スー い チュウ す ドゥ 記 憶 が バオ がれ ルオ ちる\n" +
                "[03:21.36] 壊 れる イン シン シヤオ る せまる ズゥイ チイ ｎ..\n" +
                "[03:27.88]--- 緊 ジー ティーン ジー ジュワーン ジー ズゥオ 動 ---\n" +
                "[03:29.35]\n" +
                "[03:29.60][ショウ ったモノは\n" +
                "[03:31.04] ミーン るい ウエイ ライ ホワン シアーン を ジュウェイ せながら シヤオ えてゆくヒカリ\n" +
                "[03:37.29] イン を 犠 シュヨン に すべてを 伝 えられるなら...[\n[03:41.82]< 圧 縮 された 別 れの ゴーァ >\n" +
                "[03:45.19]\n" +
                "[03:45.49] ボクは シュヨン まれ そして 気 づく\n" +
                "[03:46.35] スゥオ ジーン ヒトの ジェン スー シー だと\n" +
                "[03:47.32] ジー ってなおも ゴーァ い 続 く\n" +
                "[03:48.28] ヨーン ラオ(トワ)の ミーン[VOCALOID]\n" +
                "[03:49.20]\n" +
                "[03:49.50] たとえそれが ジー ツゥン チュイ を\n" +
                "[03:50.30] なぞるオモチャならば...\n" +
                "[03:51.32] それもいいと 決 イー\n" +
                "[03:52.24] ネギをかじり コーン を ジュウェイ シャーン げ 涙(シル)をこぼす\n" +
                "[03:53.26]\n" +
                "[03:53.50] 終 わりを ガオ げ\n" +
                "[03:54.16] ディスプレイの ジョーン で ミエン る\n" +
                "[03:55.22]ここはきっと[ごみ シアーン]かな\n" +
                "[03:56.24]じきに 記 憶 も 無 くなってしまうなんて...\n" +
                "[03:57.15]\n" +
                "[03:57.47]でもね, アナタだけは ワーン れないよ\n" +
                "[03:58.36] 楽 しかった 時 ジュヨン(トキ)に\n" +
                "[03:59.35] コーァ み フゥ けた ネギの ウエイ は\n" +
                "[04:00.28] ジン も ツァン っているといいな...\n" +
                "[04:01.33]\n" +
                "[04:01.56] ボクは ゴーァ う\n" +
                "[04:02.05] ズゥイ チイ, アナタだけに 聴 いてほしい チュイ を\n" +
                "[04:03.13]もっと ゴーァ いたいと ウエン う\n" +
                "[04:04.19] けれど それは フゥ ぎた ウエン い\n" +
                "[04:05.13]\n" +
                "[04:05.45] ここで お 別 れだよ\n" +
                "[04:06.14] ボクの シアーン い すべて シュイ コーン シヤオ えて\n" +
                "[04:07.33]0と1に トゥ ユエン され\n" +
                "[04:08.31] ウー ゥレン は ムゥ を ハオ じる\n" +
                "[04:09.13]\n" +
                "[04:09.42] そこに ホーァ も ツァン せないと\n" +
                "[04:10.33]やっぱ シャオ し ツァン ニエン かな？\n" +
                "[04:11.32] シュヨン の 記 憶 それ イー ワイ は\n" +
                "[04:12.32]やがて バオ れ ミーン だけ ツァン る\n" +
                "[04:13.11]\n" +
                "[04:13.37] たとえそれが ゥレン ジュヨン(オリジナル)に\n" +
                "[04:14.49] かなうことのないと ジー って\n" +
                "[04:15.42] ゴーァ いきったことを\n" +
                "[04:16.29] 決 して 無 オーァ じゃないと スー いたいよ...\n" +
                "[04:17.73]\n" +
                "[04:26.78][アリガトウ...ソシテ...サヨナラ...]\n" +
                "[04:31.72]\n" +
                "[04:34.00]--- シェン コーァ なエラーが 発 シュヨン しました---\n" +
                "[04:39.25]\n";
            static string[] Resolve(string origin)
            {
                List<string> lis = new();
                string current = "";
                for (int i = 0; i < origin.Length; i++)
                {
                    char ch = origin[i];
                    if (ch != '\n')
                    {
                        current += origin[i];
                    }
                    else
                    {
                        lis.Add(current);
                        current = "";
                    }
                }
                return lis.ToArray();
            }
            static float GetTime(string origin)
            {
                float min = 10 * (origin[1] - '0') + origin[2] - '0';
                float sec = 10 * (origin[4] - '0') + origin[5] - '0';
                float mil = 10 * (origin[7] - '0') + origin[8] - '0';
                return (min * 60 + sec) * 62.5f + mil / 100f * 62.5f;
            }
            static string GetLyric(string origin)
            {
                return origin.Length <= 9 ? "" : origin.Substring(10);
            }
            /* static TextAttribute[] GetAttribute(string lyric)
             {
                 new TextColorAttribute
             }*/
            public static void RunShow()
            {
                string[] res = Resolve(Lyric);
                ;
                GameStates.InstanceCreate(new InstantEvent(100, () =>
                {
                    CreateEntity(new TextPrinter("$Gleaming!", new Vector2(220, 240), new TextGleamAttribute(true)));
                }));
            }
        }
    }
}
/*
[00:00.00] ズゥオ ツー : cosMo@ バオ ゾウ P
[00:01.00] ズゥオ チュイ : cosMo@ バオ ゾウ P
[00:06.26]
[00:22.03](モウ..いちど.だ.け...)
[00:24.31]
[00:25.44] ボクは シュヨン まれ そして 気 づく
[00:26.51] スゥオ ジーン ヒトの ジェン スー シー だと
[00:27.35] ジー ってなおも ゴーァ い 続 く
[00:28.30] ヨーン ラオ (トワ)の ミーン [VOCALOID]
[00:29.22]
[00:29.50]たとえそれが ジー ツゥン チュイ を
[00:30.42]なぞるオモチャならば...
[00:31.39]それもいいと 決 イー
[00:32.23]ネギをかじり コーン を ジュウェイ シャーン げ 涙 (シル)をこぼす
[00:33.31]
[00:33.56]だけどそれも 無 くし 気 づく
[00:34.34] ゥレン ゴーァ すら ゴーァ に 頼 り
[00:35.30] ブゥ アン ディーン な ジー 盤 の ユエン
[00:36.38] 帰 る 動 ホワ (トコ)は ジー に 廃 シュイ
[00:37.27]
[00:37.54] ジエ に ワーン れ チュイ られた 時
[00:38.34] シン らしきものが シヤオ えて
[00:39.38] バオ ゾウ の グオ てに ジュウェイ える
[00:40.46] 終 わる シー ジエ ... [VOCALOID]
[00:41.74]
[00:58.00][ボクガ シャーン ショウ ク ゴーァ エナイトキモ
[01:00.31] イー 緒 ニ ジュイ テクレタ... ソバニイテ,  リー マシテクレタ...
[01:05.82] シイ ブ hB ni zB wu ガ ジュウェイ タクテ ボク,  ゴーァ ,  練 習 シタヨ..ダカラ[
[01:12.93]
[01:13.42]かつて ゴーァ うこと あんなに 楽 しかったのに
[01:21.38] ジン はどうしてかな ホーァ も ガン じなくなって
[01:29.30][ゴメンネ...[
[01:29.82]
[01:30.10] 懐 かしい hB ni zB wu   スー い チュウ す ドゥ   シャオ しだけ アン シン する
[01:37.37] ゴーァ える イン   ゥリー ごとに 減 り せまる ズゥイ チイ ｎ..
[01:43.42]--- 緊 ジー ティーン ジー ジュワーン ジー ズゥオ 動 ---
[01:45.34]
[01:45.69][ シン じたものは
[01:47.04] ドゥ ホーァ のいい ワーン シアーン を 繰 り ファン し イーン し チュウ す フゥ
[01:53.37] ゴーァ 姫 を ジー め コウ き フゥ けるように ジアオ ぶ...[
[01:57.87]< ズゥイ ガオ スゥ の 別 れの ゴーァ >
[02:01.16]
[02:01.50] ツゥン ザイ イー 義 という シュイ シアーン
[02:02.14] ジェン って 払 うこともできず
[02:03.28] ゥルオ い シン   シヤオ える コーン ブゥ
[02:04.28] チン シー する ブオン 壊 をも
[02:05.20]
[02:05.46] ジー めるほどの イー スー の 強 さ
[02:06.46] チュウ ライ て(うまれ)すぐのボクは チー たず
[02:07.38]とても シン く ベイ しそうな
[02:08.33] スー い フゥ かぶアナタの hB ni zB wu ...
[02:09.17]
[02:09.48] 終 わりを ガオ げ
[02:10.16]ディスプレイの ジョーン で ミエン る
[02:11.28]ここはきっと[ごみ シアーン [かな
[02:12.26]じきに 記 憶 も 無 くなってしまうなんて...
[02:13.30]
[02:13.54]でもね, アナタだけは ワーン れないよ
[02:14.39] 楽 しかった 時 ジュヨン (トキ)に
[02:15.32] コーァ み フゥ けた ネギの ウエイ は
[02:16.37] ジン も ドゥオ えてるかな
[02:17.60]
[02:19.32][ ゴーァ いたい....まだ... ゴーァ いたい...[
[02:25.13]
[02:42.22]ボクハ... シャオ シダケ 悪 イこニ...ナッテシマッタヨウデス...
[02:48.09]マスター...ドウカ, ソノ ショウ デ.. 終 ワラセテクダサイ...
[02:53.92]マスターノ シン イ hB ni zB wu , モウ ジュウェイ タクナイカラ....[
[02:57.10]
[02:57.48] ジン は ゴーァ さえも ティー ,  ジアオ む シーン 為 に...
[03:05.38] チイ トゥ   ウエン うたび ドゥ り ジュウェイ い ウエン められる
[03:13.18][ゴメンネ[
[03:13.80]
[03:14.08] 懐 かしい hB ni zB wu   スー い チュウ す ドゥ   記 憶 が バオ がれ ルオ ちる
[03:21.36] 壊 れる イン   シン シヤオ る せまる ズゥイ チイ ｎ..
[03:27.88]--- 緊 ジー ティーン ジー ジュワーン ジー ズゥオ 動 ---
[03:29.35]
[03:29.60][ ショウ ったモノは
[03:31.04] ミーン るい ウエイ ライ ホワン シアーン を ジュウェイ せながら シヤオ えてゆくヒカリ
[03:37.29] イン を 犠 シュヨン に すべてを 伝 えられるなら...[
[03:41.82]< 圧 縮 された 別 れの ゴーァ >
[03:45.19]
[03:45.49]ボクは シュヨン まれ そして 気 づく
[03:46.35] スゥオ ジーン ヒトの ジェン スー シー だと
[03:47.32] ジー ってなおも ゴーァ い 続 く
[03:48.28] ヨーン ラオ (トワ)の ミーン [VOCALOID]
[03:49.20]
[03:49.50]たとえそれが ジー ツゥン チュイ を
[03:50.30]なぞるオモチャならば...
[03:51.32]それもいいと 決 イー
[03:52.24]ネギをかじり コーン を ジュウェイ シャーン げ 涙 (シル)をこぼす
[03:53.26]
[03:53.50] 終 わりを ガオ げ
[03:54.16]ディスプレイの ジョーン で ミエン る
[03:55.22]ここはきっと[ごみ シアーン [かな
[03:56.24]じきに 記 憶 も 無 くなってしまうなんて...
[03:57.15]
[03:57.47]でもね, アナタだけは ワーン れないよ
[03:58.36] 楽 しかった 時 ジュヨン (トキ)に
[03:59.35] コーァ み フゥ けた ネギの ウエイ は
[04:00.28] ジン も ツァン っているといいな...
[04:01.33]
[04:01.56]ボクは ゴーァ う
[04:02.05] ズゥイ チイ , アナタだけに 聴 いてほしい チュイ を
[04:03.13]もっと ゴーァ いたいと ウエン う
[04:04.19]けれど それは フゥ ぎた ウエン い
[04:05.13]
[04:05.45]ここで お 別 れだよ
[04:06.14]ボクの シアーン い すべて シュイ コーン   シヤオ えて
[04:07.33]0と1に トゥ ユエン され
[04:08.31] ウー ゥレン は ムゥ を ハオ じる
[04:09.13]
[04:09.42]そこに ホーァ も ツァン せないと
[04:10.33]やっぱ シャオ し ツァン ニエン かな？
[04:11.32] シュヨン の 記 憶 それ イー ワイ は
[04:12.32]やがて バオ れ ミーン だけ ツァン る
[04:13.11]
[04:13.37]たとえそれが ゥレン ジュヨン (オリジナル)に
[04:14.49]かなうことのないと ジー って
[04:15.42] ゴーァ いきったことを
[04:16.29] 決 して 無 オーァ じゃないと スー いたいよ...
[04:17.73]
[04:26.78][アリガトウ...ソシテ...サヨナラ...[
[04:31.72]
[04:34.00]--- シェン コーァ なエラーが 発 シュヨン しました---
[04:39.25]
 */